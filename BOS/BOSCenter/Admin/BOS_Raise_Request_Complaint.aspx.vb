Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Net.Mail

Public Class BOS_Raise_Request_Complaint
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim vcountry, VState, VUpdatedBy, VUpdatedOn As String
    Dim QryStr As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                lblComplaintID.Text = GV.FL.AddInVar("ComplaintPrefix", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.AutoNumber") & GV.get_AutoNumber("ComplaintId", GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)

                If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                    'dis,sd,re,cust
                    GV.FL.AddInDropDownListDistinct(ddlProductService, "Title", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where ActiveStatus='Active'")
                ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                    'super admin
                Else
                    'admin and other employees 
                    GV.FL.AddInDropDownListDistinct(ddlProductService, "Title", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_sa where ActiveStatus='Active'")
                End If


                If ddlProductService.Items.Count > 0 Then
                    ddlProductService.Items.Insert(0, "Select Service")
                Else
                    ddlProductService.Items.Add("Select Service")
                End If

                ddlProductService_SelectedIndexChanged(sender, e)
                Bind()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Dim str As String


    Dim statecode As String
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            If ddlProductService.SelectedIndex = 0 Then
                lblError.Text = "Please Select Services"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            If ddlProblem.SelectedIndex = 0 Then
                lblError.Text = "Please Select Services Problem"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            If txtComments.Text = "" Then
                lblError.Text = "Please Enter Complaints"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            Dim ComplaintsPath As String = ""
            Dim Group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim LoginID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            If Not btnUpload_AgeProof.ToolTip.Trim = "" Then
                Dim fi As New FileInfo(btnUpload_AgeProof.ToolTip.Trim)
                ComplaintsPath = "~/ComplaintsDocs" & "/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "/" & lblComplaintID.Text & "/" & fi.Name
            Else
                ComplaintsPath = ""
            End If
          
            Dim result As Boolean = False


            Dim qry As String = ""


            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                qry = " Insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Complaint_Master (CompanyCode,ComplaintDate,ComplaintID,kCode,kCodeType,Product,Problem,Complaint,Attachment,ComplaintStatus,RecordDateTime,UpdatedBy,UpdatedOn) values ('" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response) & "','" & Now & "','" & GV.parseString(lblComplaintID.Text) & "','" & LoginID & "','" & Group & "','" & GV.parseString(ddlProductService.SelectedValue.Trim) & "','" & GV.parseString(ddlProblem.SelectedValue.Trim) & "','" & GV.parseString(txtComments.Text.Trim) & "','" & ComplaintsPath & "','Unalloted',Getdate(),'" & LoginID & "',getdate()) "
            ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
            Else
                qry = " Insert into " & GV.DefaultDatabase.Trim & ".dbo.BOS_Complaint_Master_SA (CompanyCode,ComplaintDate,ComplaintID,kCode,kCodeType,Product,Problem,Complaint,Attachment,ComplaintStatus,RecordDateTime,UpdatedBy,UpdatedOn) values ('" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response) & "','" & Now & "','" & GV.parseString(lblComplaintID.Text) & "','" & LoginID & "','" & Group & "','" & GV.parseString(ddlProductService.SelectedValue.Trim) & "','" & GV.parseString(ddlProblem.SelectedValue.Trim) & "','" & GV.parseString(txtComments.Text.Trim) & "','" & ComplaintsPath & "','Unalloted',Getdate(),'" & LoginID & "',getdate()) "
            End If

            If Not qry.Trim = "" Then

                result = GV.FL.DMLQueriesBulk(qry)

                If result = True Then

                    Dim destinationpath As String = "~/ComplaintsDocs/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "/" & lblComplaintID.Text
                    If Not btnUpload_AgeProof.ToolTip.Trim = "" Then

                        If Not Directory.Exists(Server.MapPath(destinationpath)) Then
                            Directory.CreateDirectory(Server.MapPath(destinationpath))
                        End If
                        If File.Exists(Server.MapPath(btnUpload_AgeProof.ToolTip.Trim)) Then
                            File.Move(Server.MapPath(btnUpload_AgeProof.ToolTip.Trim), Server.MapPath(ComplaintsPath))

                        End If
                    End If
                    Dim SendTo As String = GV.FL.AddInVar("EmailID", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & LoginID & "'")

                    If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                        'dis,sd,re,cust
                        SendTo = GV.FL.AddInVar("EmailID", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & LoginID & "'")

                    ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                        'super admin
                    Else
                        'admin and other employees 
                        SendTo = GV.FL.AddInVar("EmailId", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where [User_ID]='ADMIN'")
                    End If


                    If SendTo = "" Then
                        SendTo = "support@businessonlinesolution.in" '"Support@boscenter.in"
                    Else
                        SendTo = "support@businessonlinesolution.in;" & SendTo '"Support@boscenter.in"
                    End If

                    Dim msg As String = ""
                    msg = txtComments.Text.Trim
                    msg = msg & Environment.NewLine & "Complaint No :" & lblComplaintID.Text
                    msg = msg & Environment.NewLine & Group & "ID :" & LoginID
                    msg = msg & Environment.NewLine & "Product Service Name:" & GV.parseString(ddlProductService.SelectedValue.Trim)
                    sendNewMail(msg, ddlProductService.SelectedValue, SendTo)
                    Bind()
                    Clear()
                    ddlProductService_SelectedIndexChanged(sender, e)
                    lblError.Text = "Your Complaint Submmited Successfully.Your ComplaintId is " & lblComplaintID.Text & " ."
                    lblError.CssClass = "Successlabels"

                Else

                    lblError.Text = "Complaint Submmition Failed."
                    lblError.CssClass = "errorlabels"
                End If

            End If




        Catch ex As Exception
        End Try
    End Sub
    Public Sub Clear()
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            ddlProductService.SelectedIndex = 0
            txtComments.Text = ""
            lblComplaintID.Text = GV.FL.AddInVar("ComplaintPrefix", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.AutoNumber") & GV.get_AutoNumber("ComplaintId", GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)
            Image_AgeProof.ImageUrl = "~/images/uploadimage.png"
            btnUpload_AgeProof.Text = "Upload"
            btnCancelDeleteDocument.Enabled = False
        Catch ex As Exception

        End Try
    End Sub



    Public smtpclient As New SmtpClient
    Public message As New MailMessage
    Public Sub sendNewMail(ByVal msg As String, ByVal subject As String, ByVal sendTo As String)
        Try

            Dim attachment As System.Net.Mail.Attachment
            Dim mailAddr() As String
            Dim fromAddress As New MailAddress("support@businessonlinesolution.in", "BOS")
            smtpclient.Host = "us2.smtp.mailhostbox.com"

            smtpclient.Credentials = New System.Net.NetworkCredential("support@businessonlinesolution.in", "boscenter@123")
            SmtpClient.EnableSsl = False
            SmtpClient.Port = 25
            Message.From = fromAddress
            If Not btnUpload_AgeProof.ToolTip.Trim = "" Then
                attachment = New System.Net.Mail.Attachment(Server.MapPath(btnUpload_AgeProof.ToolTip.Trim)) 'file path
                ' attachment = New System.Net.Mail.Attachment(btnUpload_AgeProof.ToolTip.Trim) 'file path
                message.Attachments.Add(attachment)

            End If
            mailAddr = Split(sendTo, ";")
            message.To.Clear()

            For i As Integer = 0 To mailAddr.Length - 1
                message.To.Add(New MailAddress(mailAddr(i)))
            Next


            Message.Subject = subject
            Message.IsBodyHtml = True
            Message.Body = msg
            SmtpClient.Send(Message)


        Catch ex As Exception
        End Try
    End Sub


    Dim filePath As String = ""
    Dim filename As String = ""
    Dim ext As String = ""
    Protected Sub btnDeleteUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelDeleteDocument.Click
        Try

            btnDelete_Document.Visible = True
            btnDelete_Document.Text = "Yes"
            btnCancelDeleteDocument.Text = "No"

            lblDeleteInfo.CssClass = ""
            lblDeleteInfo.Text = "This Action will Remove the document Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"

            lblDeleteDocumentInfo.Text = "PanCard"
            ModalPopupExtender4.Show()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload_AgeProof.Click
        Try
            Try
                lblErrorImage_PanCard.Text = ""
                lblErrorImage_PanCard.CssClass = ""
                If (btnUpload_AgeProof.Text = "Download") Then
                    DownloadDoc(btnUpload_AgeProof.ToolTip)
                Else
                    If FileUpload_AgeProof.HasFile = True Then


                        filePath = FileUpload_AgeProof.PostedFile.FileName
                        filename = Path.GetFileName(filePath)
                        ext = Path.GetExtension(filename)

                        If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Or (UCase(ext) = UCase(".doc")) Or (UCase(ext) = UCase(".docx")) Or (UCase(ext) = UCase(".pdf")) Or (UCase(ext) = UCase(".xls")) Or (UCase(ext) = UCase(".xlsx")) Then
                            SaveDocument(FileUpload_AgeProof, btnUpload_AgeProof, btnDeleteUpload_AgeProof, "ComProof")
                            Dim fi As New FileInfo(btnUpload_AgeProof.ToolTip.ToString())
                            Dim ext As String = fi.Extension.ToUpper
                            If fi.Extension.ToUpper = UCase(".jpg") Or fi.Extension.ToUpper = UCase(".jpeg") Or fi.Extension.ToUpper = UCase(".png") Or fi.Extension.ToUpper = UCase(".gif") Then
                                Image_AgeProof.ImageUrl = btnUpload_AgeProof.ToolTip.ToString()
                                btnUpload_AgeProof.Focus()
                            Else
                                Image_AgeProof.ImageUrl = "~/images/documents.png"
                                btnUpload_AgeProof.Focus()
                            End If
                        Else
                            lblErrorImage_PanCard.Text = "Invalid Document Type."
                            lblErrorImage_PanCard.CssClass = "errorlabels"
                            btnUpload_AgeProof.Focus()
                        End If

                    Else
                        lblErrorImage_PanCard.Text = "Please Select Document To Upload."
                        lblErrorImage_PanCard.CssClass = "errorlabels"
                        btnUpload_AgeProof.Focus()
                    End If
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub



    Public Sub SaveDocument(ByVal imagUpload As FileUpload, ByVal UploadButtonName As Button, ByVal RemoveButtonName As Button, ByVal imgstr As String)
        Try
            Dim imgPath As String = ""
            If imagUpload.HasFile = True Then
                filePath = imagUpload.PostedFile.FileName
                filename = Path.GetFileName(filePath)
                ext = Path.GetExtension(filename)
                Session("ext") = ext
                If (UCase(Session("ext")) = UCase(".jpg")) Or (UCase(Session("ext")) = UCase(".jpeg")) Or (UCase(Session("ext")) = UCase(".png")) Or (UCase(Session("ext")) = UCase(".gif")) Or (UCase(Session("ext")) = UCase(".doc")) Or (UCase(Session("ext")) = UCase(".docx")) Or (UCase(Session("ext")) = UCase(".pdf")) Or (UCase(Session("ext")) = UCase(".xls")) Or (UCase(Session("ext")) = UCase(".xlsx")) Or (UCase(Session("ext")) = UCase(".zip")) Then
                    Dim completeFilePath As String = ""

                    If Not Directory.Exists(Server.MapPath("~/Temp")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp"))
                    End If
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim))
                    End If


                    completeFilePath = Server.MapPath("~/Temp" & "/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "/" & lblComplaintID.Text.Trim & "_" & imgstr & Session("ext"))
                    imgPath = "~/Temp" & "/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "/" & lblComplaintID.Text.Trim & "_" & imgstr & Session("ext")
                    imagUpload.PostedFile.SaveAs(completeFilePath)


                    If (UCase(Session("ext")) = UCase(".jpg")) Or (UCase(Session("ext")) = UCase(".jpeg")) Or (UCase(Session("ext")) = UCase(".png")) Or (UCase(Session("ext")) = UCase(".gif")) Then
                        Dim bytesInStream As Byte() = New Byte(imagUpload.PostedFile.InputStream.Length - 1) {}
                        Dim image As System.Drawing.Image = System.Drawing.Image.FromStream(imagUpload.PostedFile.InputStream)


                        Dim bmp As New Bitmap(image)
                        bmp = ImageOptimization(bmp, completeFilePath, filename, "THUMBNAIL")
                        bmp.Save(completeFilePath, System.Drawing.Imaging.ImageFormat.Jpeg)

                        'bmp.Save(completeFilePath + "/Thumbnail_" + filename, System.Drawing.Imaging.ImageFormat.Jpeg)
                    End If

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
        'If imagetype.Equals("THUMBNAIL") Then
        '    If image.Height > image.Width Then
        '        '================ Potrait================================
        '        reqWidth = 113
        '        reqHeight = 170
        '    Else
        '        '================ Landscape================================
        '        reqWidth = 170
        '        reqHeight = 113
        '    End If
        'ElseIf imagetype.Equals("WATERMARK") Then
        '    If image.Height > image.Width Then
        '        '================ Potrait================================
        '        reqWidth = 500
        '        reqHeight = 873
        '    Else
        '        '================ Landscape================================
        '        reqWidth = 800
        '        reqHeight = 560
        '    End If
        'Else
        '    reqWidth = image.Width
        '    reqHeight = image.Height
        'End If

        'float scaleFactor = getScalFactor(image.Width, image.Height, reqWidth, reqHeight);
        'var newWidth = (int)(image.Width * scaleFactor);
        'var newHeight = (int)(image.Height * scaleFactor);
        'var thumbnailImg = new Bitmap(newWidth, newHeight);

        'var thumbnailImg = new Bitmap(reqWidth, reqHeight);
        Dim thumbnailImg = Resize_Image(DirectCast(image, Bitmap), reqWidth, reqHeight)

        'var thumbGraph = Graphics.FromImage(thumbnailImg);
        'thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
        'thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
        'thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
        '''/var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
        'var imageRectangle = new Rectangle(0, 0, reqWidth, reqHeight);
        'thumbGraph.DrawImage(image, imageRectangle);
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
    Protected Sub btnDelete_Document_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete_Document.Click
        Try
            '===========Delete Uploaded Document Popup=============================
            If btnDelete_Document.Text = "Yes" Then
                Dim qry As String = ""
                If lblDeleteDocumentInfo.Text = "ComProof" Then
                    Dim imgpath As String = btnUpload_AgeProof.ToolTip
                    File.Delete(Server.MapPath(imgpath))

                    btnUpload_AgeProof.ToolTip = ""
                    btnUpload_AgeProof.Text = "Upload"
                    btnDeleteUpload_AgeProof.Enabled = False
                    Image_AgeProof.ImageUrl = "~/images/uploadimage.png"
                    btnDeleteUpload_AgeProof.Focus()

                    lblDeleteInfo.Text = "Document Removed Successfully."
                    lblDeleteInfo.CssClass = "successlabels"

                End If
                btnDelete_Document.Visible = False
                btnDelete_Document.Text = "OK"
                btnCancelDeleteDocument.Text = "OK"

            Else

            End If
            ModalPopupExtender4.Show()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDeleteUpload_Signature_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelDeleteDocument.Click
        Try

            btnDelete_Document.Visible = True
            btnDelete_Document.Text = "Yes"
            btnCancelDeleteDocument.Text = "No"
            lblDeleteInfo.CssClass = ""
            lblDeleteInfo.Text = "This Action will Remove the Signature Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"
            lblDeleteDocumentInfo.Text = "ComProof"
            ModalPopupExtender4.Show()


        Catch ex As Exception
        End Try
    End Sub


    Protected Sub ddlProductService_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProductService.SelectedIndexChanged
        Try
            ddlProblem.Items.Clear()
            
            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                'dis,sd,re,cust
                GV.FL.AddInDropDownListDistinct(ddlProblem, "ProductProblem", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ComplaintVSProblem_Master where Product='" & GV.parseString(ddlProductService.SelectedValue.Trim) & "'")
            ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                'super admin
            Else
                'admin and other employees 
                GV.FL.AddInDropDownListDistinct(ddlProblem, "ProductProblem", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_ComplaintVSProblem_Master_Sa where Product='" & GV.parseString(ddlProductService.SelectedValue.Trim) & "'")
            End If

            If ddlProblem.Items.Count > 0 Then
                ddlProblem.Items.Insert(0, "Select Problem")
            Else
                ddlProblem.Items.Add("Select Problem")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Public Sub Bind()
        Try
            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                'dis,sd,re,cust
                str = "select '0' as 'SrNo',ComplaintID,Product,Problem,Complaint,ComplaintStatus as Status from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Complaint_Master where ComplaintStatus='Unalloted'  AND kCode='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' order by rid desc "
            ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                'super admin
            Else
                'admin and other employees 
                str = "select '0' as 'SrNo',ComplaintID,Product,Problem,Complaint,ComplaintStatus as Status from " & GV.DefaultDatabase.Trim & ".dbo.BOS_Complaint_Master_SA where ComplaintStatus='Unalloted'  AND  CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response) & "'  order by rid desc "
                'str = "select '0' as 'SrNo',ComplaintID,Product,Problem,Complaint,ComplaintStatus as Status from BosCenter_DB.dbo.BOS_Complaint_Master_SA where ComplaintStatus='Unalloted'  AND kCode='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' and CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response) & "'  order by rid desc "
            End If

            ds = GV.FL.OpenDsWithSelectQuery(str)
            GridView1.DataSource = ds.Tables(0)
            GridView1.DataBind()
            GV.FL.showSerialnoOnGridView(GridView1, 0)
        Catch ex As Exception
        End Try
    End Sub
End Class