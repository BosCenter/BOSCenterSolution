
Imports System.IO
Imports System.Net

Public Class ProfileInfo
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim VUser_Name, VUser_ID, VImagePath, VUser_Password, VUser_Type, VAccountStatus, VUser_CreationDate, VLoggedinStatus, VLastLoginTime, VUpdatedOn, VUpdatedBy As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""
    Dim fromTime, totime As String
    Dim DS As New DataSet


    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try


            Response.Redirect("SuperAdminDashBoard.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    

    Private Sub Clear()
        Try
            btnUpload.Text = "Upload"
            btnDeleteUpload.Enabled = False
            Image1.ImageUrl = "..\images\logo_login2.png"
            lblError.CssClass = ""
            VUser_Name = ""
            VUser_ID = ""
            VUser_Password = ""
            VUser_Type = ""
            VAccountStatus = ""
            VUser_CreationDate = ""
            VLoggedinStatus = ""
            VLastLoginTime = ""
            VUpdatedOn = ""
            VUpdatedBy = ""
            txtUser_Name.Text = ""
            txtUser_ID.Text = ""
            lblloginIdError.Text = ""
            lblloginIdError.CssClass = ""

            ddlUser_Type.SelectedIndex = 0

            lblErrorImageError.Text = ""
            lblErrorImageError.CssClass = ""

            Session("EditFlag") = 0
            btnSave.Text = "Save"
            txtUser_ID.ReadOnly = False
            lblError.Text = ""
            btnSave.Enabled = True

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    

    Protected Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                GV.FL.AddInDropDownListDistinct(ddlUser_Type, "Group_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Group_Master")
                ddlUser_Type.Items.Insert(0, ":::: Select Group ::::")


                FileUpload1.Enabled = True

                DS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where User_ID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
                If DS.Tables(0).Rows.Count > 0 Then

                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("ImagePath")) Then
                        If Not DS.Tables(0).Rows(0).Item("ImagePath") = "" Then
                            '  VImagePath = DS.Tables(0).Rows(0).Item("ImagePath").ToString()
                            'If File.Exists(DS.Tables(0).Rows(0).Item("ImagePath").ToString()) Then
                            'Else
                            '    Image1.ImageUrl = "~/images/MissingIcon3.png"
                            'End If
                            Image1.ImageUrl = DS.Tables(0).Rows(0).Item("ImagePath").ToString()
                            btnUpload.ToolTip = DS.Tables(0).Rows(0).Item("ImagePath").ToString()
                            Image1.Visible = True
                            btnUpload.Text = "Download"
                        Else
                            Image1.ImageUrl = "..\images\logo_login2.png"
                            btnUpload.Text = "Upload"
                            btnDeleteUpload.Enabled = False
                        End If

                    Else
                        'VImagePath = ""
                        Image1.ImageUrl = "..\images\logo_login2.png"
                        btnUpload.Text = "Upload"
                        btnDeleteUpload.Enabled = False

                    End If
                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("User_ID")) Then
                        If Not DS.Tables(0).Rows(0).Item("User_ID") = "" Then
                            txtUser_ID.Text = DS.Tables(0).Rows(0).Item("User_ID").ToString()
                        Else
                            txtUser_ID.Text = ""
                        End If
                    Else
                        txtUser_ID.Text = ""
                    End If

                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("User_Name")) Then
                        If Not DS.Tables(0).Rows(0).Item("User_Name") = "" Then
                            txtUser_Name.Text = DS.Tables(0).Rows(0).Item("User_Name").ToString()
                        Else
                            txtUser_Name.Text = ""
                        End If
                    Else
                        txtUser_Name.Text = ""
                    End If



                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("EmailID")) Then
                        If Not DS.Tables(0).Rows(0).Item("EmailID") = "" Then
                            txtEmailID.Text = DS.Tables(0).Rows(0).Item("EmailID").ToString()
                        Else
                            txtEmailID.Text = ""
                        End If
                    Else
                        txtEmailID.Text = ""
                    End If


                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("MobileNo")) Then
                        If Not DS.Tables(0).Rows(0).Item("MobileNo") = "" Then
                            txtMobileNo.Text = DS.Tables(0).Rows(0).Item("MobileNo").ToString()
                        Else
                            txtMobileNo.Text = ""
                        End If
                    Else
                        txtMobileNo.Text = ""
                    End If



                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("User_Type")) Then
                        If Not DS.Tables(0).Rows(0).Item("User_Type") = "" Then
                            ddlUser_Type.Text = DS.Tables(0).Rows(0).Item("User_Type").ToString()
                        Else
                            ddlUser_Type.SelectedIndex = 0
                        End If
                    Else
                        ddlUser_Type.SelectedIndex = 0
                    End If



                End If

            End If



        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub txtUser_ID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtUser_ID.TextChanged
        Try

            If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where User_ID='" & txtUser_ID.Text.Trim & "'") > 0 Then 'Change where condition according to Criteria 
                lblloginIdError.Text = "Not Available."
                lblloginIdError.CssClass = "errorlabels-sm"
            Else
                lblloginIdError.Text = "Available."
                lblloginIdError.CssClass = "Successlabels_sm"
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Try
            lblErrorImageError.Text = ""
            lblDialogMsg.CssClass = ""
            lblDialogMsg.Text = ""
            If (btnUpload.Text = "Download") Then
                DownloadDoc(btnUpload.ToolTip)

            Else
                If FileUpload1.HasFile = True Then
                    SaveImage(FileUpload1, btnUpload, btnDeleteUpload)
                    Image1.ImageUrl = btnUpload.ToolTip.ToString()
                    lblDialogMsg.Text = "Image Uploaded Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                    btnok.Visible = False
                    btnCancel.Text = "Ok"
                    ModalPopupExtender1.Show()

                    If UCase(btnUpload.Text = "Download") Then
                        VImagePath = (btnUpload.ToolTip)
                    Else
                        VImagePath = ""
                    End If
                    GV.FL.DMLQueries("update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details set ImagePath='" & VImagePath & "' where User_ID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")

                Else
                    lblDialogMsg.Text = "No file Selected for photo"
                    lblDialogMsg.CssClass = "errorlabels"
                    btnok.Enabled = False
                    btnCancel.Text = "Ok"
                    ModalPopupExtender1.Show()
                    'lblErrorImageError.Text = "No file Selected for photo"
                    'lblErrorImageError.CssClass = "errorlabels"
                    btnSave.Focus()
                End If
            End If

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

                    If Not Directory.Exists(Server.MapPath("..\OperatorImages")) Then
                        Directory.CreateDirectory(Server.MapPath("..\OperatorImages"))
                    End If

                    completeFilePath = Server.MapPath("..\OperatorImages\" & txtUser_ID.Text.Trim & Session("ext"))
                    'imgPath = Server.MapPath("~\Docs\" & txtPatientID.Text & "\" & txtPrescriptionNo.Text & "\Doppler" & Session("ext"))
                    imgPath = "..\OperatorImages\" & txtUser_ID.Text.Trim & Session("ext")

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

    Protected Sub btnDeleteUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteUpload.Click
        Try
            Try
                btnDelete_Document.Visible = True
                btnDelete_Document.Text = "Yes"
                btnCancelDeleteDocument.Text = "No"

                lblDeleteInfo.CssClass = ""
                lblDeleteInfo.Text = "This Action will Remove the Logo Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"

                ModalPopupExtender4.Show()

            Catch ex As Exception
                GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

            End Try
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnDelete_Document_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete_Document.Click
        Try
            '===========Delete Uploaded Document Popup=============================
            If btnDelete_Document.Text = "Yes" Then
                Dim qry As String = ""
                'VCompanyCode = GV.parseString(txtUser_ID.Text.Trim)
                'VCompanyCode = GV.get_Admin_SessionVariables("CompanyCode", Request, Response)

                ' If lblDeleteDocumentInfo.Text = "IncomeProof" Then

                Dim imgpath As String = btnUpload.ToolTip
                If Directory.Exists(Server.MapPath(imgpath)) Then
                    File.Delete(Server.MapPath(imgpath))
                End If

                qry = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details set ImagePath=''  where User_ID='" & GV.parseString(txtUser_ID.Text.Trim) & "' "

                btnUpload.ToolTip = ""
                btnUpload.Text = "Upload"
                btnDeleteUpload.Enabled = False
                Image1.ImageUrl = "..\images\logo_login2.png"
                btnDeleteUpload.Focus()

                lblDeleteInfo.Text = "Document Removed Successfully."
                lblDeleteInfo.CssClass = "successlabels"
                'End If


                If Not qry = "" Then
                    GV.FL.DMLQueries(qry)
                End If

                btnDelete_Document.Visible = False
                btnDelete_Document.Text = "OK"
                btnCancelDeleteDocument.Text = "OK"
                lblErrorImageError.Text = ""
                lblErrorImageError.CssClass = ""
                'btnCancelDeleteDocument.Attributes("style") = "display:none"

            Else

            End If
            ModalPopupExtender4.Show()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub
End Class