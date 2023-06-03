Imports System.Data.OleDb
Imports System.Data
Imports System.Net
Imports System.IO
Public Class BOS_SA_Link_Virtual_Accounts
    Inherits System.Web.UI.Page
    Dim GV As New GlobalVariable("ADMIN")
    Dim aa() As String
    Dim qry As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ddlTransferTo_SelectedIndexChanged(sender, e)
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ddlTransferToAgent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTransferToAgent.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            lblError.Visible = False
            If ddlTransferToAgent.SelectedIndex = 0 Then
                txt_account_no.Text = ""
                txt_upi_id.Text = ""
                txt_ifsc.Text = ""

            Else
                Dim aa() As String
                Dim qry As String = ""

                If ddlTransferToAgent.SelectedValue.ToString.Contains(":") Then
                    aa = ddlTransferToAgent.SelectedValue.ToString.Split(":")
                    qry = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & aa(0) & "'"
                End If

                Dim ds As DataSet = New DataSet
                ds = GV.FL.OpenDsWithSelectQuery(qry)
                If Not ds Is Nothing Then
                    If ds.Tables.Count > 0 Then
                        If ds.Tables(0).Rows.Count > 0 Then
                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("easeBuzz_Virtual_Acc_No")) Then
                                If Not ds.Tables(0).Rows(0).Item("easeBuzz_Virtual_Acc_No").ToString = "" Then
                                    txt_account_no.Text = ds.Tables(0).Rows(0).Item("easeBuzz_Virtual_Acc_No").ToString
                                Else
                                    txt_account_no.Text = ""
                                End If
                            Else
                                txt_account_no.Text = ""
                            End If
                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("easeBuzz_IFSC")) Then
                                If Not ds.Tables(0).Rows(0).Item("easeBuzz_IFSC").ToString = "" Then
                                    txt_ifsc.Text = ds.Tables(0).Rows(0).Item("easeBuzz_IFSC").ToString
                                Else
                                    txt_ifsc.Text = ""
                                End If
                            Else
                                txt_ifsc.Text = ""
                            End If
                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("easeBuzz_Virtual_UPI_ID")) Then
                                If Not ds.Tables(0).Rows(0).Item("easeBuzz_Virtual_UPI_ID").ToString = "" Then
                                    txt_upi_id.Text = ds.Tables(0).Rows(0).Item("easeBuzz_Virtual_UPI_ID").ToString
                                Else
                                    txt_upi_id.Text = ""
                                End If
                            Else
                                txt_upi_id.Text = ""
                            End If
                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("easeBuzz_QrCode")) Then

                                If Not ds.Tables(0).Rows(0).Item("easeBuzz_QrCode").ToString() = "" Then
                                    filePath = ds.Tables(0).Rows(0).Item("easeBuzz_QrCode").ToString()
                                    filename = Path.GetFileName(filePath)
                                    ext = Path.GetExtension(filename)
                                    If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then

                                        btn_UPIQRCode_Upload.ToolTip = GV.parseString(ds.Tables(0).Rows(0).Item("easeBuzz_QrCode").ToString())
                                        Image_UPIQRCode_url.ImageUrl = btn_UPIQRCode_Upload.ToolTip
                                    End If
                                    btn_UPIQRCode_Upload.Text = "Download"
                                    btn_UPIQRCode_DeleteUpload.Enabled = True
                                Else
                                    btn_UPIQRCode_Upload.ToolTip = ""
                                    Image_UPIQRCode_url.ImageUrl = "~/images/uploadimage.png"
                                End If
                            Else
                                btn_UPIQRCode_Upload.ToolTip = ""
                                Image_UPIQRCode_url.ImageUrl = "~/images/uploadimage.png"
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Public Function RandomOTP() As String
        Dim finalString As String = ""
        Try
            Dim chars = "0123456789"
            Dim stringChars = New Char(3) {}
            Dim random = New Random()
            For i As Integer = 0 To stringChars.Length - 1
                stringChars(i) = chars(random.[Next](chars.Length))
            Next
            finalString = New String(stringChars)
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
        Return finalString
    End Function


    Protected Sub ddlTransferTo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTransferTo.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            lblError.Visible = False
            'If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Admin" Then

            If ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Select Admin ::::".Trim.ToUpper Then
                GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, " CompanyCode +':'+ isnull(CompanyName,'') ", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where Status='Active' ")
                If ddlTransferToAgent.Items.Count > 0 Then
                    ddlTransferToAgent.Items.Insert(0, ":::: Select Admin ::::")
                Else
                    ddlTransferToAgent.Items.Add(":::: Select Admin ::::")
                End If
                lblAgentType.Text = "Admin"
                'End If
            End If


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btn_update_Click(sender As Object, e As EventArgs) Handles btn_update.Click
        Try
            Dim v_Accountno, v_IFSC, v_UPIID, v_UPIQRCode As String

            v_Accountno = ""
            v_IFSC = ""
            v_UPIID = ""
            v_UPIQRCode = ""

            lblError.Text = ""
            lblError.CssClass = ""
            lblError.Visible = False

            If ddlTransferToAgent.Items.Count <= 0 Then
                lblError.Text = "Please Select Agent."
                lblError.CssClass = "errorLabels"
                lblError.Visible = True

                ddlTransferToAgent.Focus()
                Exit Sub
            End If


            If ddlTransferToAgent.SelectedIndex = 0 Then
                lblError.Text = "Please Select Agent."
                lblError.CssClass = "errorLabels"
                lblError.Visible = True

                ddlTransferToAgent.Focus()
                Exit Sub
            End If



            If GV.parseString(txt_account_no.Text) = "" Then
                lblError.Text = "Please Enter Account No."
                lblError.CssClass = "errorLabels"
                lblError.Visible = True

                txt_account_no.Focus()
                Exit Sub
            Else
                v_Accountno = GV.parseString(txt_account_no.Text)
            End If
            If GV.parseString(txt_ifsc.Text) = "" Then
                lblError.Text = "Please Enter IFSC."
                lblError.CssClass = "errorLabels"
                lblError.Visible = True
                txt_ifsc.Focus()
                Exit Sub
            Else
                v_IFSC = GV.parseString(txt_ifsc.Text)
            End If
            If GV.parseString(txt_upi_id.Text) = "" Then
                lblError.Text = "Please Enter UPI ID."
                lblError.Visible = True
                lblError.CssClass = "errorLabels"
                txt_upi_id.Focus()
                Exit Sub
            Else
                v_UPIID = GV.parseString(txt_upi_id.Text)
            End If

            If GV.parseString(btn_UPIQRCode_Upload.ToolTip.Trim) = "" Then
                lblError.Text = "Please Upload UPI QR Code."
                lblError.Visible = True
                lblError.CssClass = "errorLabels"
                btn_UPIQRCode_Upload.Focus()
                Exit Sub
            End If

            If ddlTransferToAgent.SelectedValue.ToString.Contains(":") Then

                aa = ddlTransferToAgent.SelectedValue.ToString.Split(":")

                qry = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & aa(0) & "'"

                If Not btn_UPIQRCode_Upload.ToolTip.Trim = "" Then
                    Dim fi As New FileInfo(btn_UPIQRCode_Upload.ToolTip.Trim)
                    v_UPIQRCode = "~/AdminDocuments/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "/" & aa(0) & "/" & fi.Name
                Else
                    v_UPIQRCode = ""
                End If

                Dim insrtQry As String = ""
                If btn_update.Text.Trim.ToUpper = "Update".Trim.ToUpper Then
                    insrtQry = "update  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ClientRegistration Set easeBuzz_Virtual_Acc_No='" & v_Accountno & "', easeBuzz_IFSC='" & v_IFSC & "', easeBuzz_Virtual_UPI_ID='" & v_UPIID & "', easeBuzz_QrCode='" & v_UPIQRCode & "'  where CompanyCode='" & aa(0) & "'"
                End If
                Dim rowsAffected As Integer = 0
                Dim result As Boolean
                result = GV.FL.DMLQueriesBulk(insrtQry)
                lbl_result.Visible = True
                If result = True Then
                    Dim destinationpath As String = "~/AdminDocuments/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "/" & aa(0)

                    If Not btn_UPIQRCode_Upload.ToolTip.Trim = "" Then

                        If Not Directory.Exists(Server.MapPath(destinationpath)) Then
                            Directory.CreateDirectory(Server.MapPath(destinationpath))
                        End If

                        If Not btn_UPIQRCode_Upload.ToolTip.Trim = "" Then
                            If File.Exists(Server.MapPath(btn_UPIQRCode_Upload.ToolTip.Trim)) Then
                                File.Move(Server.MapPath(btn_UPIQRCode_Upload.ToolTip.Trim), Server.MapPath(v_UPIQRCode))
                            End If
                        End If
                    End If
                    If btn_update.Text.Trim.ToUpper = "Update".Trim.ToUpper Then
                        lblError.Visible = True
                        lblError.Text = "Record Updated Successfully"
                        lblError.CssClass = "successLabels"
                    Else
                        lblError.Visible = True
                        lblError.Text = "Record Updation Failed"
                        lblError.CssClass = "errorLabels"
                    End If
                End If


            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub
    Protected Sub btn_UPIQRCode_Upload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_UPIQRCode_Upload.Click

        Try
            lblErrorImage_UPIQRCode.Text = ""
            lblErrorImage_UPIQRCode.CssClass = ""
            If (btn_UPIQRCode_Upload.Text = "Download") Then
                DownloadDoc(btn_UPIQRCode_Upload.ToolTip)
            Else

                If FileUpload_UPIQRCode.HasFile = True Then

                    filePath = FileUpload_UPIQRCode.PostedFile.FileName
                    filename = Path.GetFileName(filePath)
                    ext = Path.GetExtension(filename)

                    If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then
                        SaveImage(FileUpload_UPIQRCode, btn_UPIQRCode_Upload, btn_UPIQRCode_DeleteUpload, "UPIQRCode")
                        Dim fi As New FileInfo(btn_UPIQRCode_Upload.ToolTip.ToString())
                        Dim ext As String = fi.Extension.ToUpper
                        If fi.Extension.ToUpper = UCase(".jpg") Or fi.Extension.ToUpper = UCase(".jpeg") Or fi.Extension.ToUpper = UCase(".png") Or fi.Extension.ToUpper = UCase(".gif") Then
                            Image_UPIQRCode_url.ImageUrl = btn_UPIQRCode_Upload.ToolTip.ToString()
                            btn_UPIQRCode_Upload.Focus()

                        End If
                    Else
                        Image_UPIQRCode_url.ImageUrl = "~/images/uploadimage.png"
                        btn_UPIQRCode_Upload.Focus()
                    End If

                Else
                    lblErrorImage_UPIQRCode.Text = "No file Selected for UPI QR Code"
                    btn_update.Focus()
                End If

            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try

    End Sub
    Public Sub SaveImage(ByVal imagUpload As FileUpload, ByVal UploadButtonName As Button, ByVal RemoveButtonName As Button, ByVal imgstr As String)
        Try
            Dim UPIQR As String = ""
            Dim imgPath As String = ""
            If imagUpload.HasFile = True Then
                filePath = imagUpload.PostedFile.FileName
                filename = Path.GetFileName(filePath)
                ext = Path.GetExtension(filename)
                Session("ext") = ext
                If (UCase(Session("ext")) = UCase(".jpg")) Or (UCase(Session("ext")) = UCase(".jpeg")) Or (UCase(Session("ext")) = UCase(".png")) Or (UCase(Session("ext")) = UCase(".gif")) Then


                    Dim completeFilePath As String


                    If Not Directory.Exists(Server.MapPath("~/Temp")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp"))
                    End If

                    If Not Directory.Exists(Server.MapPath("~/Temp/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim))
                    End If
                    If ddlTransferToAgent.SelectedValue.ToString.Contains(":") Then

                        aa = ddlTransferToAgent.SelectedValue.ToString.Split(":")

                        UPIQR = aa(0)

                        completeFilePath = Server.MapPath("~/Temp" & "/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "/" & aa(0) & "_" & imgstr & Session("ext"))
                        imgPath = "~/Temp" & "/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "/" & aa(0) & "_" & imgstr & Session("ext")

                        imagUpload.PostedFile.SaveAs(completeFilePath)


                        UploadButtonName.ToolTip = imgPath
                        UploadButtonName.Text = "Download"
                        RemoveButtonName.Enabled = True
                    Else

                    End If
                End If
            Else

            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub
    Protected Sub btn_UPIQRCode_DeleteUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_UPIQRCode_DeleteUpload.Click
        Try
            btnDelete_Document.Visible = True
            btnDelete_Document.Text = "Yes"
            btnCancelDeleteDocument.Text = "No"
            lblDeleteInfo.CssClass = ""
            lblDeleteInfo.Text = "This Action will Remove the Photo Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"
            lblDeleteDocumentInfo.Text = "UPIQRCode"
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
            lblErrorImage_UPIQRCode.Text = "Download"

            response.[End]()
            reset()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Dim filePath As String = ""
    Dim filename As String = ""
    Dim ext As String = ""
    Protected Sub btnDelete_Document_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete_Document.Click
        Dim v_UPIQR As String
        Dim qry As String = ""
        Try
            '===========Delete Uploaded Document Popup=============================
            If btnDelete_Document.Text = "Yes" Then
                If ddlTransferToAgent.SelectedValue.ToString.Contains(":") Then

                    aa = ddlTransferToAgent.SelectedValue.ToString.Split(":")

                    v_UPIQR = aa(0)
                    'VCompanyCode = get_Admin_SessionVariables("CompanyCode", Request, Response)

                    If lblDeleteDocumentInfo.Text = "UPIQRCode" Then

                        Dim imgpath As String = btn_UPIQRCode_Upload.ToolTip
                        If File.Exists(Server.MapPath(imgpath)) Then
                            File.Delete(Server.MapPath(imgpath))
                        End If


                        qry = "update BOS_ClientRegistration  set easeBuzz_QrCode='' where CompanyCode='" & aa(0) & "' "

                        GV.FL.DMLQueriesBulk(qry)
                        btn_UPIQRCode_Upload.ToolTip = ""
                        btn_UPIQRCode_Upload.Text = "Upload"
                        btn_UPIQRCode_DeleteUpload.Enabled = False
                        Image_UPIQRCode_url.ImageUrl = "..\images\uploadimage.png"
                        btn_UPIQRCode_DeleteUpload.Focus()

                        lblDeleteInfo.Text = "UPI QR Code Removed Successfully."
                        lblDeleteInfo.CssClass = "successlabels"
                    End If
                    btnDelete_Document.Visible = False
                    btnDelete_Document.Text = "OK"
                    btnCancelDeleteDocument.Text = "OK"
                Else

                End If
            End If
            ModalPopupExtender4.Show()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Public Sub reset()
        Try

            lblError.Text = ""
            lblError.CssClass = ""
            lblError.Visible = False

            If ddlTransferToAgent.Items.Count > 0 Then
                ddlTransferToAgent.SelectedIndex = 0
            End If

            txt_account_no.Text = ""
            txt_ifsc.Text = ""
            txt_upi_id.Text = ""
            btn_UPIQRCode_Upload.ToolTip = ""
            btn_UPIQRCode_Upload.Text = "Upload"
            btn_UPIQRCode_DeleteUpload.Enabled = False
            Image_UPIQRCode_url.ImageUrl = "..\images\uploadimage.png"
            btn_UPIQRCode_Upload.Text = "Upload"


            txt_account_no.CssClass = "form-control"
            txt_ifsc.CssClass = "form-control"
            txt_upi_id.CssClass = "form-control"
            ddlTransferToAgent.CssClass = "form-control"

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btn_reset_Click(sender As Object, e As EventArgs) Handles btn_reset.Click
        Try
            reset()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

End Class